import "../../styles/reports/statistic.css";

interface Props {
    title: string;
    value: number | string;
    icon?: string;
    color?: string;
}

export default function StatisticCard({
    title,
    value,
    icon = "📊",
    color = "#3498db"
}: Props) {

    return (
        <div
            className="stat-card"
            style={{ borderTop: `4px solid ${color}` }}
        >
            <div className="stat-icon">
                {icon}
            </div>

            <div className="stat-info">
                <h3>{value}</h3>
                <p>{title}</p>
            </div>
        </div>
    );
}