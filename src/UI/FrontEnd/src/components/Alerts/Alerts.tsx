import { FC } from "react";
import { AlertItem } from "../../models/AlertItem";
import './Alerts.css';
import ComponentTitle from "../ComponentTitle/ComponentTitle";

type AlertProps = {
  alertItems: AlertItem[];
}

const Alerts: FC<AlertProps> = ({ alertItems }) => {
  return (
    <>
      <ComponentTitle title="Alerts" />

      <div className="alert-table-container">
        <table>
          <tbody>
            <tr>
              <td>RuleName</td>
              <td>Severity</td>
              <td>Count</td>
              <td>Explanation</td>
            </tr>

            {alertItems.map((item) => {
              return (
                <tr key={Math.random()} onClick={()=> alert(item.RuleName)}>
                  <td>{item.RuleName}</td>
                  <td>{item.Severity}</td>
                  <td>{item.Count}</td>
                  <td>{item.Explanation}</td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>

    </>
  );
}

export default Alerts;