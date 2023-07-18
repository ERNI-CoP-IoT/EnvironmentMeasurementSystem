import { FC } from "react";
import { SensorItem } from "../../models/SensorItem";
import './Sensor.css';

type SensorProps = {
  item: SensorItem;
};

const Sensor: FC<SensorProps> = ({ item }) => {
  return (
    <div className="sensor-container">
      <div><b>Sensor name:</b> {item.name}</div>
      <div>Description: {item.description}</div>
      <div className="sensor-detail-container">
        <div>
          <div>Temperature: {item.temperature}</div>
          <div>Humidity: {item.humidity}</div>
        </div>
        <div>
          <div>CO2: {item.co2}</div>
          <div>NOx: {item.nox}</div>
        </div>
        <div>
          <div>PM2.5: {item.pm25}</div>
          <div>PM10: {item.pm10}</div>
        </div>
        <div>
          <div>Pressure: {item.pressure}</div>
          <div>Battery level: {item.batterylevel}</div>
        </div>
      </div>
    </div>
  );
}

export default Sensor;