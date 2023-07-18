import {
    AiFillCaretDown,
    AiFillCaretUp,
    AiOutlineHistory,
    AiOutlineHome,
    AiOutlineMoneyCollect,
    AiOutlineUser
} from 'react-icons/ai';
import { FaCog, FaOpencart } from 'react-icons/fa';
import { SidebarItem } from '../../models/SidebarItem';

export const SidebarData: SidebarItem[] = [
    {
        title: 'Dashboard',
        path: '/',
        icon: <AiOutlineHome />,
        // iconClosed: <AiFillCaretDown />,
        // iconOpened: <AiFillCaretUp />,
        // subnav: [
        //     {
        //         title: 'Users',
        //         path: '/devices',
        //         icon: <AiOutlineUser />
        //     },
        //     {
        //         title: 'Revenue',
        //         path: '/overview/revenue',
        //         icon: <AiOutlineMoneyCollect />
        //     }
        // ]
    },
    {
        title: 'Devices',
        path: '/devices',
        icon: <FaOpencart />
    },
    {
        title: 'Rules',
        path: '/rules',
        icon: <AiOutlineHistory />
    },
    {
        title: 'Maintenance',
        path: '/maintenance',
        icon: <FaCog />
    }
];