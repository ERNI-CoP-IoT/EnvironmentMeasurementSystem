import { Routes, Route } from 'react-router-dom';
import Dashboard from './components/Dashboard/Dashboard';
import Devices from './components/Devices/Devices';
import Home from './components/Home/Home';
import Rules from './components/Rules/Rules';
import Maintenance from './components/Maintenance/Maintenance';
import Graph from './components/Graph/Graph';
import Graph2 from './components/Graph2/Graph2';

const RouteComponent = () => {
  return (
    <Routes>
      <Route path="/" element={<Home />}>
        <Route path="/" element={<Dashboard />}>
          <Route path="/" element={<Graph />}/>
          <Route path="/Graph2" element={<Graph2 />}/>
        </Route>
        <Route path="devices" element={<Devices />} />
        <Route path="/rules" element={<Rules />} />
        <Route path="/maintenance" element={<Maintenance />} />
      </Route>
    </Routes>
  );
}
export default RouteComponent;