import "../../styles/menus/menu.css";
import type { Menu } from "../../types/menu";
import { canManage } from "../../utils/auth";
interface Props {
    menu: Menu;
    selected?: boolean;
    onClick: () => void;
    onEdit: (menu: Menu) => void;
    onDelete: (id: number) => void;
}

export default function MenuCard({
    menu,
    selected,
    onClick,
    onEdit,
    onDelete
}: Props) {

    const getIcon = (name: string) => {
        const n = name.toLowerCase();
        if (n.includes("vip")) return "👑";
        if (n.includes("trưa")) return "🍱";
        if (n.includes("tối")) return "🍽️";
        return "📋";
    };

    const handleDelete = () => {
        const ok = window.confirm("Bạn có chắc muốn xóa menu này không?");
        if (ok) onDelete(menu.menuId);
    };

    return (
        <div className={`menu-card ${selected ? "active" : ""}`} onClick={onClick}>

            <div className="menu-icon">
                {getIcon(menu.name)}
            </div>

            <div className="menu-body">
                <h3>{menu.name}</h3>
                <p>{menu.description}</p>
            </div>

            <div className="menu-footer">
                <span className={menu.status ? "status-open" : "status-close"}>
                    {menu.status ? "Hoạt động" : "Ngừng"}
                </span>
            </div>
            {canManage() && (

            <div className="menu-actions">
                <button
                    className="btn-edit"
                    onClick={(e) => {
                        e.stopPropagation();
                        onEdit(menu);
                    }}
                >
                    Sửa
                </button>

                <button
                    className="btn-delete"
                    onClick={(e) => {
                        e.stopPropagation();
                        handleDelete();
                    }}
                >
                    Xóa
                </button>
            </div>
            )}
        </div>
    );
}