import { FC } from "react";
import './Logout.css';
import { PublicClientApplication } from "@azure/msal-browser";
import { config } from './../../Config';
import { AiOutlineLogout } from "react-icons/ai";

const Logout: FC = () => {
  async function Logout() {
    const publicClientApplication = new PublicClientApplication({
      auth: {
        clientId: config.appId,
        redirectUri: config.redirectUri,
        authority: config.authority
      },
      cache: {
        cacheLocation: "sessionStorage",
        storeAuthStateInCookie: true
      }
    });

    try {
      const currentAccounts = publicClientApplication.getAllAccounts();
      currentAccounts.forEach(element => {
        console.log('CURRENT ACCOUNTS ' + element.homeAccountId);
      });
      const logoutRequest = {
        account: publicClientApplication.getAccountByHomeId(currentAccounts[0].homeAccountId),
        mainWindowRedirectUri: config.redirectUri,
      };

      await publicClientApplication.logoutPopup(logoutRequest);
    } catch {
    }
  }

  return (
    <button className="logout-button" onClick={Logout}><AiOutlineLogout /></button>
  )
}

export default Logout;