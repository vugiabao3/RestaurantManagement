
import "../../styles/foods/searchDish.css";
interface Props {

    keyword:string;

    setKeyword:
    (value:string)=>void;

    onSearch:
    ()=>void;
}

export default function SearchDish(
{
    keyword,
    setKeyword,
    onSearch
}:Props
){

    return(

        <div className="search-box">

            <input
                placeholder="Tìm món ăn..."
                value={keyword}
                onChange={(e)=>
                    setKeyword(
                        e.target.value
                    )
                }
            />

            <button
                onClick={onSearch}
            >
                Search
            </button>

        </div>

    );
}