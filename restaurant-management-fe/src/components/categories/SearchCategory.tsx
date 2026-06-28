import "../../styles/categories/searchCategory.css";
import { canManage } from "../../utils/auth";
interface Props {

    keyword: string;

    setKeyword: (
        value: string
    ) => void;

    onSearch: () => void;

    onAdd: () => void;
}

export default function SearchCategory({

    keyword,

    setKeyword,

    onSearch,

    onAdd

}: Props) {

    return (

        <div className="search-category">

            <input

                type="text"

                placeholder="Search category..."

                value={keyword}

                onChange={(e) =>
                    setKeyword(
                        e.target.value
                    )
                }

            />

            <button
                className="search-btn"
                onClick={onSearch}
            >

                Search

            </button>
            {canManage() && (
            <button
                className="add-btn"
                onClick={onAdd}
            >

                + Add Category

            </button>
            )}

        </div>

    );

}