import { createRoot } from "react-dom/client";
import Tag from "../components/Tag";

it('renders tag without crashing', async () => {
    const div = document.createElement('div');
    const root = createRoot(div);
    root.render(
      <Tag/>);
    await new Promise(resolve => setTimeout(resolve, 1000));
  });