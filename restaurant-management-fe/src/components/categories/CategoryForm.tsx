import { useState, useEffect } from "react";

import "../../styles/categories/categoryForm.css";

interface Props {

    category?: any;

    onSubmit: (data: any) => void;
}

export default function CategoryForm({

    category,

    onSubmit

}: Props) {
    const [menuId, setMenuId] =
    useState<number>(0);

    const [name, setName] =
        useState("");

    const [description,
        setDescription] =
        useState("");

    const [status,
        setStatus] =
        useState(true);

    useEffect(() => {

    if(category){

        setName(category.name);

        setDescription(
            category.description
        );

        setStatus(
            category.status
        );

        setMenuId(
            category.menuId ?? 0
        );

    }

},[category]);

    const handleSubmit = (
        e: React.FormEvent
    ) => {

        e.preventDefault();

        onSubmit({

            name,

    description,

    status,

    menuId


        });

    };

    return (

        <form
            className="category-form"
            onSubmit={handleSubmit}
        >

            <div className="form-group">

                <label>

                    Category Name

                </label>

                <input

                    value={name}

                    onChange={(e) =>
                        setName(
                            e.target.value
                        )
                    }

                    required

                />

            </div>

            <div className="form-group">

                <label>

                    Description

                </label>

                <textarea

                    value={description}

                    onChange={(e) =>
                        setDescription(
                            e.target.value
                        )
                    }

                />

            </div>
            <div className="form-group">

    <label>

        Menu ID

    </label>

    <input

        type="number"

        value={menuId}

        onChange={(e)=>

            setMenuId(
                Number(e.target.value)
            )

        }

    />

</div>

            <div className="checkbox-group">

                <input

                    type="checkbox"

                    checked={status}

                    onChange={(e) =>
                        setStatus(
                            e.target.checked
                        )
                    }

                />

                <span>

                    Active

                </span>

            </div>

            <button
                type="submit"
            >

                {
                    category
                        ? "Update Category"
                        : "Add Category"
                }

            </button>

        </form>

    );

}