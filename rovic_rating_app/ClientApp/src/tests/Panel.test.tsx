import { createRoot } from "react-dom/client";
import Panel from "../components/Panel";

it('renders panel without crashing', async () => {
    const div = document.createElement('div');
    const root = createRoot(div);
    root.render(
      <Panel/>);
    await new Promise(resolve => setTimeout(resolve, 1000));
  });