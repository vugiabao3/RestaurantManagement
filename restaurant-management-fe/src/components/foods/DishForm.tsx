import { useState } from "react";
import "../../styles/foods/dishForm.css";

export default function DishForm({
    onSubmit,
    initialData
}: any) {

    const [name, setName] =
        useState(
            initialData?.name || ""
        );

    const [price, setPrice] =
        useState(
            initialData?.price || 0
        );

    const [description,
        setDescription] =
        useState(
            initialData?.description || ""
        );

    const [imageUrl,
        setImageUrl] =
        useState(
            initialData?.imageUrl || ""
        );

    const [categoryId,
        setCategoryId] =
        useState(
            initialData?.categoryId || 1
        );

    const [status,
        setStatus] =
        useState(
            initialData?.status ?? true
        );

    return (

        <form
            className="dish-form"
            onSubmit={(e) => {

                e.preventDefault();

                onSubmit({
                    name,
                    price,
                    description,
                    status,
                    imageUrl,
                    categoryId
                });

            }}
        >

            <div className="form-group">

                <label>
                    Name
                </label>

                <input
                    value={name}
                    onChange={(e) =>
                        setName(
                            e.target.value
                        )
                    }
                />

            </div>

            <div className="form-group">

                <label>
                    Price
                </label>

                <input
                    type="number"
                    value={price}
                    onChange={(e) =>
                        setPrice(
                            Number(
                                e.target.value
                            )
                        )
                    }
                />

            </div>

            <div className="form-group">

                <label>
                    Image URL
                </label>

                <input
                    value={imageUrl}
                    onChange={(e) =>
                        setImageUrl(
                            e.target.value
                        )
                    }
                />

            </div>

            <div className="form-group">

                <label>
                    Category Id
                </label>

                <input
                    type="number"
                    value={categoryId}
                    onChange={(e) =>
                        setCategoryId(
                            Number(
                                e.target.value
                            )
                        )
                    }
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
                    Available
                </span>

            </div>

            <button type="submit">
                Save Dish
            </button>

        </form>

    );
}