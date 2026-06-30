import "../../styles/reports/report.css";

interface Props {
    title: string;
    description: string;
    icon?: string;
    onClick: () => void;
}

export default function ReportCard({
    title,
    description,
    icon = "📊",
    onClick
}: Props) {

    return (
        <div className="report-card" onClick={onClick}>

            <div className="report-icon">
                {icon}
            </div>

            <h3>{title}</h3>

            <p>{description}</p>

        </div>
    );
}