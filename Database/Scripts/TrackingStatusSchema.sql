USE master;
GO

IF DB_ID('SelfRestaurant') IS NOT NULL
BEGIN
    ALTER DATABASE SelfRestaurant SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE SelfRestaurant;
END
GO

CREATE DATABASE SelfRestaurant;
GO

USE SelfRestaurant;
GO

-- 1. Bàn ăn
CREATE TABLE dbo.restaurant_table (
    table_id     INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    table_number NVARCHAR(10) NOT NULL UNIQUE,
    [status]     NVARCHAR(20) NOT NULL DEFAULT N'Không hoạt động',
    capacity     INT NULL,
    created_at   DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT chk_rt_status CHECK ([status] IN (N'Trống', N'Đang dùng', N'Không hoạt động'))
);
GO

-- 2. Người dùng nội bộ
CREATE TABLE dbo.[users] (
    user_id       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    username      NVARCHAR(50) NOT NULL UNIQUE,
    password_hash NVARCHAR(255) NOT NULL,
    full_name     NVARCHAR(100) NOT NULL,
    [role]        NVARCHAR(20) NOT NULL,
    is_active     BIT NOT NULL DEFAULT 1,
    create_at     DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT chk_users_role CHECK ([role] IN (N'Đầu bếp', N'Quản lý', N'Thu ngân', N'Admin'))
);
GO

-- 3. Món ăn
CREATE TABLE dbo.menu_item (
    item_id               INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [name]                NVARCHAR(100) NOT NULL,
    image_url             NVARCHAR(255) NULL,
    price                 DECIMAL(12,0) NOT NULL,
    cooking_time_standard INT NOT NULL,
    category              NVARCHAR(50) NULL,
    is_available          BIT NOT NULL DEFAULT 1,
    create_at             DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);
GO

-- 4. Đơn hàng đang hoạt động tại bàn
CREATE TABLE dbo.orders (
    order_id  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    table_id  INT NOT NULL,
    [status]  NVARCHAR(10) NOT NULL DEFAULT 'ACTIVE',
    create_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    closed_at DATETIME NULL,

    CONSTRAINT chk_orders_status CHECK ([status] IN ('ACTIVE', 'PAID', 'CANCELLED')),
    CONSTRAINT fk_order_table FOREIGN KEY (table_id)
        REFERENCES dbo.restaurant_table(table_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- 5. Chi tiết món trong đơn hàng
CREATE TABLE dbo.order_item (
    order_item_id  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    order_id       INT NOT NULL,
    item_id        INT NOT NULL,
    quantity       INT NOT NULL DEFAULT 1,
    original_price DECIMAL(12,0) NOT NULL,
    discount_rate  DECIMAL(5,2) NOT NULL DEFAULT 0.00,
    unit_price     DECIMAL(12,0) NOT NULL,
    item_status    NVARCHAR(10) NOT NULL DEFAULT 'PENDING',
    is_delayed     BIT NOT NULL DEFAULT 0,
    ordered_at     DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    completed_at   DATETIME NULL,

    CONSTRAINT chk_oi_quantity CHECK (quantity > 0),
    CONSTRAINT chk_oi_item_status CHECK (item_status IN ('PENDING', 'COOKING', 'DONE', 'CANCELLED')),
    CONSTRAINT fk_oi_order FOREIGN KEY (order_id)
        REFERENCES dbo.orders(order_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT fk_oi_item FOREIGN KEY (item_id)
        REFERENCES dbo.menu_item(item_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- 6. Thông báo món hoàn thành gửi cho Tablet tại bàn
CREATE TABLE dbo.notification (
    notification_id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    order_item_id   INT NOT NULL UNIQUE,
    table_id        INT NOT NULL,
    content         NVARCHAR(500) NOT NULL,
    is_displayed    BIT NOT NULL DEFAULT 0,
    queue_status    NVARCHAR(10) NOT NULL DEFAULT 'PENDING',
    create_at       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    displayed_at    DATETIME NULL,

    CONSTRAINT chk_noti_queue CHECK (queue_status IN ('PENDING', 'SENT', 'FAILED')),
    CONSTRAINT fk_noti_oi FOREIGN KEY (order_item_id)
        REFERENCES dbo.order_item(order_item_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT fk_noti_table FOREIGN KEY (table_id)
        REFERENCES dbo.restaurant_table(table_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- 7. Nhật ký món chậm nội bộ bếp
CREATE TABLE dbo.kitchen_delay_log (
    log_id           INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    order_item_id    INT NOT NULL,
    chef_user_id     INT NOT NULL,
    delay_reason     NVARCHAR(30) NOT NULL,
    delay_priority   NVARCHAR(10) NOT NULL,
    delay_duration   INT NULL,
    delay_notes      NVARCHAR(255) NULL,
    is_auto_detected BIT NOT NULL DEFAULT 0,
    [status]         NVARCHAR(10) NOT NULL DEFAULT 'ACTIVE',
    started_at       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    resolved_at      DATETIME NULL,

    CONSTRAINT chk_kdl_reason CHECK (delay_reason IN (N'Nguyên liệu thiếu', N'Đơn hàng dồn', N'Thiếu nhân sự', N'Khác')),
    CONSTRAINT chk_kdl_priority CHECK (delay_priority IN ('HIGH', 'MEDIUM', 'LOW')),
    CONSTRAINT chk_kdl_status CHECK ([status] IN ('ACTIVE', 'RESOLVED', 'CANCELLED')),
    CONSTRAINT chk_kdl_delay_notes CHECK (delay_reason <> N'Khác' OR (delay_reason = N'Khác' AND LEN(delay_notes) >= 10)),
    CONSTRAINT fk_kdl_oi FOREIGN KEY (order_item_id)
        REFERENCES dbo.order_item(order_item_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT fk_kdl_user FOREIGN KEY (chef_user_id)
        REFERENCES dbo.[users](user_id)
        ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- INDEX
CREATE INDEX idx_orders_table_status ON dbo.orders(table_id, [status]);
CREATE INDEX idx_order_item_order_status ON dbo.order_item(order_id, item_status);
CREATE INDEX idx_order_item_delayed ON dbo.order_item(is_delayed, item_status);
CREATE INDEX idx_notification_queue ON dbo.notification(table_id, queue_status);
CREATE INDEX idx_notification_displayed ON dbo.notification(table_id, is_displayed);
CREATE INDEX idx_kdl_status_started ON dbo.kitchen_delay_log([status], started_at);
GO
