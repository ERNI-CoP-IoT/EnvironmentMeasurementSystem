import { FC, useState } from 'react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import { SidebarItem } from '../../models/SidebarItem';
import './Submenu.css';

type SidebarLinkProps = {
    item: SidebarItem;
};

const SidebarLink = styled(Link)`
    display: flex;
    justify-content: space-between;
    align-items: center;
    height: 3.75rem;
    font-size: 13px;
    padding: 2rem;
    text-decoration: none;
    color: #ffffff;

    &:hover {
        background-color: #1f1f1b;
        border-left: 4px solid #83837c;
    }
`;

const DropdownLink = styled(Link)`
    display: flex;
    justify-content: flex-start;
    align-items: center;
    height: 3.75rem;
    font-size: 1.125rem;
    padding-left: 3rem;
    text-decoration: none;
    color: #ffffff;

    &:hover {
        background-color: #83837c;
    }
`;

const Submenu: FC<SidebarLinkProps> = ({ item }) => {
    const [subnav, setSubnav] = useState(false);
    const showSubnav = () => { setSubnav(!subnav);};

    return (
        <>
            <SidebarLink to={item.path} onClick={showSubnav}>
                <div className="sidebarItem">
                    <div className="sidebarLabel">{item.icon}</div>
                    <div>{item.title}</div>
                </div>
                <div>{item?.subnav && subnav ? item?.iconOpened : item?.iconClosed}</div>
            </SidebarLink>
            {subnav &&
                item?.subnav?.map((subnavItem, index) => {
                    return (
                        <DropdownLink to={subnavItem.path} key={index}>
                            {subnavItem.icon}
                            <div className="sidebarLabel">{item.title}</div>
                        </DropdownLink>
                    );
                })}
        </>
    );
};

export default Submenu;