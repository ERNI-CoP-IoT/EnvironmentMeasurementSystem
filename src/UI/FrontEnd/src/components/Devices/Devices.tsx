import { FC, useEffect, useState } from "react";
import Sensor from "../Sensor/Sensor";

import { SensorItem } from "../../models/SensorItem";

const Devices: FC = () => {
  const [sensors, setSensors] = useState<SensorItem[]>([]);
  
  useEffect(() => {
    fetch('http://localhost:7039/api/GetSensors')
      .then(response => response.json())
      .then(data => {
        console.log(data);
        const sensorItems: SensorItem[] = data.map((item: any) => {
          return {
            temperature: item.Temperature,
            humidity: item.Humidity,
            co2: item.CO2,
            nox: item.NOx,
            pm25: item.Pm25,
            pm10: item.Pm10,
            pressure: item.Pressure,
            batterylevel: item.BatteryLevel,
            description: item.Description,
            name: item.Name
          };
        });

        setSensors(sensorItems);
      })
      .catch(error => {
        console.error(error);
      });
  }, []);

  return (
    <div className="main-container">
      <h2>Sensors</h2>
      {sensors.map((sensor) => {
        return (
          <Sensor key={Math.random()} item={sensor}></Sensor>
        );
      })}
    </div>
  );
}

export default Devices;