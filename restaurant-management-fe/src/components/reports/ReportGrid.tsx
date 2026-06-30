import "../../styles/reports/report.css";

interface Props {
    children: React.ReactNode;
}

export default function ReportGrid({ children }: Props) {
    return (
        <div className="report-grid">
            {children}
        </div>
    );
}