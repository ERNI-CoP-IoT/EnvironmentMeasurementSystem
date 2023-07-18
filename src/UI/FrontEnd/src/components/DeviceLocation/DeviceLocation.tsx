import { FC } from "react";
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import './DeviceLocation.css';
import 'leaflet/dist/leaflet.css'
import { MarkerItem } from "../../models/MarkerItem";
import ComponentTitle from "../ComponentTitle/ComponentTitle";

type MapProps = {
  center: [number, number];
  zoom: number;
  markers: MarkerItem[];
};

const DeviceLocation: FC<MapProps> = ({ center, zoom, markers }) => {
  return (
    <>
      <ComponentTitle title="Device locations" />
      <MapContainer center={center} zoom={zoom} scrollWheelZoom={false}>
        <TileLayer
          attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
        {markers.map((item) => {
          return (
            <Marker key={Math.random()} position={[item.posX, item.posY]}>
              <Popup>
                {item.message}
              </Popup>
            </Marker>
          );
        })}
      </MapContainer>
    </>

  );
}

export default DeviceLocation;