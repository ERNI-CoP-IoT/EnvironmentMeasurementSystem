import { FC } from "react";
import ComponentTitle from "../ComponentTitle/ComponentTitle";
import { DevicesSummaryItem } from "../../models/DevicesSummaryItem";
import './DeviceStatistics.css';

type DevicesSummaryProps = {
  devicesSummaryItems: DevicesSummaryItem[];
}

const DeviceStatistics: FC<DevicesSummaryProps> = ({ devicesSummaryItems }) => {
  return (
    <>
      <ComponentTitle title="Device statistics" />
      <div className="statistics-container">
        <label className="statistics-label">All devices</label>
        {devicesSummaryItems.map((item) => {
          return (
            <div key={Math.random()}>
              <label className="statistics-label" style={{ color: item.Colour }}>{item.Count} </label>
              <label>{item.Severity}</label>
            </div>
          );
        })}
      </div>
    </>
  );
}

export default DeviceStatistics;