import type { Category } from "../../types/category";

interface Props{
    categories: Category[];
    onSelectCategory:(category: Category)=>void;
}

export default function MenuCategoryList({
    categories,
    onSelectCategory
}: Props) {


    if (categories.length === 0) {

        return (
        <div className="category-grid">

            {categories.map((c) => (
                <div
                    key={c.categoryId}
                    className="category-card"
                    onClick={() => onSelectCategory(c)}
                >
                    <h3>{c.name}</h3>
                    <p>{c.description}</p>
                </div>
            ))}

        </div>
    );
    }

    return (

        <div className="menu-category-grid">

            {categories.map(category => (

                <div
                    key={category.categoryId}
                    className="menu-category-card"
                     onClick={() => onSelectCategory(category)}
                >
                    <div className="menu-category-icon">

        🍽️

    </div>

                    <h3>
                        {category.name}
                    </h3>

                    <p>
                        {category.description}
                    </p>

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
                                : "Ngừng"
                        }
                    </span>

                </div>

            ))}

        </div>

    );

}