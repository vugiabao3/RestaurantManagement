import { useState, useEffect } from "react";
import { createMenu, updateMenu } from "../../api/menuApi";
import "../../styles/menus/menuForm.css";

export default function MenuForm({ menu, onSuccess }: any) {

    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [status, setStatus] = useState(true);

    useEffect(() => {
        if (menu) {
            setName(menu.name);
            setDescription(menu.description);
            setStatus(menu.status);
        }
    }, [menu]);

    const handleSubmit = async (e: any) => {
        e.preventDefault();

        const data = { name, description, status };

        if (menu) {
            await updateMenu(menu.menuId, data);
            alert("Cập nhật thành công");
        } else {
            await createMenu(data);
            alert("Thêm thành công");
        }

        onSuccess();
    };

    return (
        <form className="menu-form" onSubmit={handleSubmit}>

            <input
                placeholder="Tên menu"
                value={name}
                onChange={(e) => setName(e.target.value)}
            />

            <textarea
                placeholder="Mô tả"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
            />

            <select
                value={status ? "true" : "false"}
                onChange={(e) => setStatus(e.target.value === "true")}
            >
                <option value="true">Hoạt động</option>
                <option value="false">Ngừng</option>
            </select>

            <button type="submit">
                {menu ? "Cập nhật" : "Thêm"}
            </button>

        </form>
    );
}