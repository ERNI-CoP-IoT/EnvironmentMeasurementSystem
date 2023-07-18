import { Link, Outlet } from "react-router-dom";
import ComponentTitle from "../ComponentTitle/ComponentTitle";
import './Telemetry.css';

const Telemetry: React.FC = () => {  
  return (
    <>
      <ComponentTitle title="Telemetry" />
      <div>
        <nav>
          <ul className="tabs">
            <li>
              <Link to="/">Graph</Link>
            </li>
            <li>
              <Link to="/Graph2">Graph 2</Link>
            </li>
          </ul>
        </nav>
      </div>
      <Outlet></Outlet>
    </>
);
}

export default Telemetry;