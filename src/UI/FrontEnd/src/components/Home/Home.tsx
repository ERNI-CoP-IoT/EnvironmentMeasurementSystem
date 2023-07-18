import { FC } from "react";
import Sidebar from "../Sidebar/Sidebar";
import { Outlet } from "react-router-dom";
import Footer from "../Footer/Footer";
import './Home.css';

const Home: FC = () => {
  return (
      <div className="home-container">
        <Sidebar></Sidebar>
        <Outlet></Outlet>
        <Footer></Footer>
      </div>
  );
}

export default Home;