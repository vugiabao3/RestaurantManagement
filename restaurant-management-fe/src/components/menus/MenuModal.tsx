import "../../styles/menus/menuModal.css";

export default function MenuModal({ open, children, onClose }: any) {

    if (!open) return null;

    return (
        <div className="modal-overlay" onClick={onClose}>
            <div className="modal-box" onClick={(e) => e.stopPropagation()}>
                {children}
            </div>
        </div>
    );
}