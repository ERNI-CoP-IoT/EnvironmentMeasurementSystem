import { FC, useEffect, useState } from "react";
import DeviceLocation from "../DeviceLocation/DeviceLocation";
import Alerts from "../Alerts/Alerts";
import './Dashboard.css'
import { MarkerItem } from "../../models/MarkerItem";
import { AlertItem } from "../../models/AlertItem";
import { DevicesSummaryItem } from "../../models/DevicesSummaryItem";
import DeviceStatistics from "../DeviceStatistics/DeviceStatistics";
import Telemetry from "../Telemetry/Telemetry";
import Analytics from "../Analytics/Analytics";

const Dashboard: FC = () => {
  const mapCenter: [number, number] = [51.505, -0.09];// Coordenadas de Londres
  const mapZoom = 10;

  const [markers, setMarkers] = useState<MarkerItem[]>([]);
  const [alerts, setAlerts] = useState<AlertItem[]>([]);
  const [summary, setSummary] = useState<DevicesSummaryItem[]>([]);

  useEffect(() => {
    fetch('http://localhost:7039/api/GetMarkers')
      .then(response => response.json())
      .then(data => {
        const markersItems: MarkerItem[] = data.map((item: any) => {
          return {
            message: item.Message,
            posX: item.PosX,
            posY: item.PosY,
          };
        });

        setMarkers(markersItems);
      })
      .catch(error => {
        console.error(error);
      });

    fetch('http://localhost:7039/api/GetAlerts')
      .then(response => response.json())
      .then(data => {
        const alertsItems: AlertItem[] = data.map((item: any) => {
          return {
            RuleName: item.RuleName,
            Severity: item.Severity,
            Count: item.Count,
            Explanation: item.Explanation
          };
        });

        setAlerts(alertsItems);
      })
      .catch(error => {
        console.error(error);
      });
  }, []);

  useEffect(() => {
    const uniqueSeverities = Array.from(new Set(alerts.map(item => item.Severity)));    
    let response : DevicesSummaryItem[] = [];

    uniqueSeverities.forEach(element => {
      response.push(
        {
          Severity: element,
          Count: alerts.filter(i=> i.Severity == element).length,
          Colour: element == 'Critical' ? 'Red' : 'Yellow'
        });
    });

    response.push({
      Severity: 'Total',
      Count: alerts.length,
      Colour: 'White'
    });

    response.push({
      Severity: 'Connected',
      Count: alerts.length,
      Colour: 'White'
    });

    response.push({
      Severity: 'Offline',
      Count: 0,
      Colour: 'White'
    });

    setSummary(response);
  }, [alerts]);

  return (
    <div className='main-container'>
      <div className="dashboard-container">
        <div className="child-container device-statistics-container">
          <DeviceStatistics devicesSummaryItems={summary}></DeviceStatistics>
        </div>
        <div className="child-container">
          <DeviceLocation center={mapCenter} zoom={mapZoom} markers={markers}></DeviceLocation>
        </div>
        <div className="child-container alerts-container">
          <Alerts alertItems={alerts}></Alerts>
        </div>
      </div>
      <div className="dashboard-container">
        <div className="child-container telemetry-container">
          <Telemetry></Telemetry>
        </div>
        <div className="child-container">
          <Analytics></Analytics>
        </div>
      </div>
    </div>
  );
}

export default Dashboard;


