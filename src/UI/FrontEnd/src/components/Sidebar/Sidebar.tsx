import { FC } from 'react';
import { IconContext } from 'react-icons';
import { SidebarData } from './SidebarData';
import Submenu from '../Submenu/Submenu';
import './Sidebar.css'
import Logout from '../Logout/Logout';

const Sidebar: FC = () => {
    return (
        <IconContext.Provider value={{ color: '#fff' }}>
            <div className="header-bar">
                <h2>Dashboard</h2>
                <Logout/>
            </div>
            <div className="sidebar">
                <div className="sidebarwrap">
                    {SidebarData.map((item, index) => {
                        return <Submenu item={item} key={index} />;
                    })}
                </div>
            </div>
        </IconContext.Provider>
    );
};

export default Sidebar;