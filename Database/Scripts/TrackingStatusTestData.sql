USE SelfRestaurant;
GO

DECLARE @tableId INT;
DECLARE @chefId INT;
DECLARE @itemComGa INT;
DECLARE @itemMiXao INT;
DECLARE @itemSalad INT;
DECLARE @orderId INT;
DECLARE @orderItemCooking INT;
DECLARE @orderItemDone INT;

INSERT INTO dbo.restaurant_table (table_number, [status], capacity)
VALUES (N'B01', N'Đang dùng', 4);
SET @tableId = SCOPE_IDENTITY();

INSERT INTO dbo.[users] (username, password_hash, full_name, [role], is_active)
VALUES ('chef01', '123456', N'Nguyễn Văn Bếp', N'Đầu bếp', 1);
SET @chefId = SCOPE_IDENTITY();

INSERT INTO dbo.[users] (username, password_hash, full_name, [role], is_active)
VALUES ('admin01', '123456', N'Quản trị viên', N'Admin', 1);

INSERT INTO dbo.menu_item ([name], image_url, price, cooking_time_standard, category, is_available)
VALUES (N'Cơm gà nướng', N'/images/com-ga-nuong.jpg', 45000, 15, N'Món chính', 1);
SET @itemComGa = SCOPE_IDENTITY();

INSERT INTO dbo.menu_item ([name], image_url, price, cooking_time_standard, category, is_available)
VALUES (N'Mì xào bò', N'/images/mi-xao-bo.jpg', 50000, 12, N'Món chính', 1);
SET @itemMiXao = SCOPE_IDENTITY();

INSERT INTO dbo.menu_item ([name], image_url, price, cooking_time_standard, category, is_available)
VALUES (N'Salad Caesar', N'/images/salad-caesar.jpg', 35000, 8, N'Món khai vị', 1);
SET @itemSalad = SCOPE_IDENTITY();

INSERT INTO dbo.orders (table_id, [status])
VALUES (@tableId, 'ACTIVE');
SET @orderId = SCOPE_IDENTITY();

-- Món 1: PENDING
INSERT INTO dbo.order_item (order_id, item_id, quantity, original_price, discount_rate, unit_price, item_status, is_delayed, ordered_at)
VALUES (@orderId, @itemComGa, 2, 45000, 0, 45000, 'PENDING', 0, DATEADD(MINUTE, -2, GETDATE()));

-- Món 2: COOKING và đang bị chậm nội bộ
INSERT INTO dbo.order_item (order_id, item_id, quantity, original_price, discount_rate, unit_price, item_status, is_delayed, ordered_at)
VALUES (@orderId, @itemMiXao, 1, 50000, 0, 50000, 'COOKING', 1, DATEADD(MINUTE, -20, GETDATE()));
SET @orderItemCooking = SCOPE_IDENTITY();

INSERT INTO dbo.kitchen_delay_log (order_item_id, chef_user_id, delay_reason, delay_priority, delay_duration, delay_notes, is_auto_detected, [status], started_at)
VALUES (@orderItemCooking, @chefId, N'Đơn hàng dồn', 'HIGH', 8, N'Bếp đang có nhiều đơn hàng cần xử lý cùng lúc', 0, 'ACTIVE', DATEADD(MINUTE, -5, GETDATE()));

-- Món 3: DONE và có notification
INSERT INTO dbo.order_item (order_id, item_id, quantity, original_price, discount_rate, unit_price, item_status, is_delayed, ordered_at, completed_at)
VALUES (@orderId, @itemSalad, 1, 35000, 0, 35000, 'DONE', 0, DATEADD(MINUTE, -25, GETDATE()), DATEADD(MINUTE, -5, GETDATE()));
SET @orderItemDone = SCOPE_IDENTITY();

INSERT INTO dbo.notification (order_item_id, table_id, content, is_displayed, queue_status, create_at)
VALUES (@orderItemDone, @tableId, N'Salad Caesar của bạn đã sẵn sàng! Vui lòng di chuyển đến Quầy Trả Món để nhận đồ ăn. Chúc quý khách ngon miệng!', 0, 'PENDING', GETDATE());
GO

SELECT * FROM dbo.restaurant_table;
SELECT * FROM dbo.[users];
SELECT * FROM dbo.menu_item;
SELECT * FROM dbo.orders;
SELECT * FROM dbo.order_item;
SELECT * FROM dbo.kitchen_delay_log;
SELECT * FROM dbo.notification;
GO
