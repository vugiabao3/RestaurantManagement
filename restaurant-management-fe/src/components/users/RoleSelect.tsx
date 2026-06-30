import React from "react";

interface Props {
    value: string;
    onChange: (value: string) => void;
}

export default function RoleSelect({ value, onChange }: Props) {
    return (
        <select
            value={value}
            onChange={(e) => onChange(e.target.value)}
        >
            <option value="Admin">Admin</option>
            <option value="Chef">Chef</option>
            <option value="Cashier">Cashier</option>
            <option value="Customer">Customer</option>
        </select>
    );
}