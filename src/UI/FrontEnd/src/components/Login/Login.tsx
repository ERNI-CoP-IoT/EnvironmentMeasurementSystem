import { FC, useState } from "react";
import './Login.css';
import RouteComponent from "../../RouteComponent";
import { PublicClientApplication } from "@azure/msal-browser";
import { config } from './../../Config';

const Login: FC = () => {
 
  const [authenticated, setAuthenticated] = useState(false);

  async function Login() {
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
      await publicClientApplication.loginPopup({
        scopes: config.scopes,
        prompt: "select_account"
      });
      setAuthenticated(true);
    } catch (error) {
      setAuthenticated(false);
    }
  }

  return (
    <>
      {(authenticated) ?
        <RouteComponent />
        : 
        <div className="login-container">
          <h2>Welcome to Environment Measurement System</h2>
          <button onClick={Login}>Login</button>
        </div>
        
      }
    </>
  )
}


export default Login;