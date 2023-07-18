import { FC } from "react";
import ComponentTitle from "../ComponentTitle/ComponentTitle";
import { Bar, BarChart, CartesianGrid, Pie, PieChart, XAxis, YAxis } from "recharts";

const data = [
  { name: 'Geeksforgeeks', students: 400 },
  { name: 'Technical scripter', students: 700 },
  { name: 'Geek-i-knack', students: 200 },
  { name: 'Geek-o-mania', students: 1000 }
];

const pieData = [
  {
     name: "Apple",
     value: 54.85
  },
  {
     name: "Samsung",
     value: 47.91
  },
  {
     name: "Redmi",
     value: 16.85
  },
  {
     name: "One Plus",
     value: 16.14
  },
  {
     name: "Others",
     value: 10.25
  }
];

const Analytics: FC = () => {
  return (
    <>
      <ComponentTitle title="Analytics" />
      <label> Top rules with active alerts
      </label>
      <BarChart width={700} height={150} data={data}>
            <Bar dataKey="students" fill="green" />
            <CartesianGrid stroke="#ccc" />
            <XAxis dataKey="name" />
            <YAxis />
        </BarChart>
      <label> Alert by device type
      </label>
      <PieChart width={100} height={100}>
      <Pie
              data={pieData}
              color="#000000"
              dataKey="value"
              nameKey="name"
              cx={50}
              cy={50}
              fill={'#FFD669'}>
          </Pie>
      </PieChart>
      
    </>
  );
}

export default Analytics;