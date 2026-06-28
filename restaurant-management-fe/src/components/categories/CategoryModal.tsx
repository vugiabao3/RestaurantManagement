import "../../styles/categories/categoryModal.css";

interface Props {

    open: boolean;

    title: string;

    children: React.ReactNode;

    onClose: () => void;
}

export default function CategoryModal({

    open,

    title,

    children,

    onClose

}: Props) {

    if (!open) return null;

    return (

        <div className="category-modal-overlay">

            <div className="category-modal">

                <h2>
                    {title}
                </h2>

                {children}

                <button
                    className="close-btn"
                    onClick={onClose}
                >
                    Close
                </button>

            </div>

        </div>

    );

}