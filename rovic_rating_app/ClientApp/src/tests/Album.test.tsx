import { createRoot } from "react-dom/client";
import Album from "../components/Album";

it('renders album without crashing', async () => {
    const div = document.createElement('div');
    const root = createRoot(div);
    root.render(
      <Album/>);
    await new Promise(resolve => setTimeout(resolve, 1000));
  });