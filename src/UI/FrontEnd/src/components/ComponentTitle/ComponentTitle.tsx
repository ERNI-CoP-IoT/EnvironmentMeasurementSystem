import { FC } from "react";
import './ComponentTitle.css';
import 'leaflet/dist/leaflet.css'

type TitleProps = {
  title: string;
};

const ComponentTitle: FC<TitleProps> = ({ title }) => {
  return (
    <h3>
      {title}
    </h3>
  );
}

export default ComponentTitle;