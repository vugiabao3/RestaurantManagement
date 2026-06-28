import type { Category }
from "../../types/category";

interface Props {
    category: Category;
    onClick: () => void;
    selected?: boolean;
}
const getIcon = (name:string)=>{

    const n = name.toLowerCase();

    if(n.includes("việt")) return "🍜";

    if(n.includes("âu")) return "🍕";

    if(n.includes("uống")) return "🍹";

    if(n.includes("tráng")) return "🍰";

    if(n.includes("hải")) return "🦞";

    return "🍽️";
};

export default function CategoryCard({
    category,
    onClick,
    selected
}: Props) {

    return (


        <div
    className={
        selected
            ? `category-card active color-${category.categoryId % 5}`
            : `category-card color-${category.categoryId % 5}`
    }
    onClick={onClick}
>

            <div className="category-header">

                <div className="category-icon">
    {getIcon(category.name)}
</div>

                <div>

                    <h3>
                        {category.name}
                    </h3>

                    <p>
                        {category.description}
                    </p>

                </div>

            </div>

            <div className="category-footer">

                <span
                    className={
                        category.status
                        ? "status-active"
                        : "status-inactive"
                    }
                >
                    {
                        category.status
                        ? "Đang hoạt động"
                        : "Ngừng hoạt động"
                    }
                </span>

            </div>

        </div>
    );
}