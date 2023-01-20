import { BrowserRouter } from "react-router-dom";

export function App() {
  return <div>Hello there!</div>;
}

export function WrappedApp() {
  return (
    <BrowserRouter>
      <App />
    </BrowserRouter>
  );
}
