import { createRoot } from "react-dom/client";
import RateMovie from "../components/RateMovie";

it('renders ratemovie without crashing', async () => {
    const div = document.createElement('div');
    const root = createRoot(div);
    root.render(
      <RateMovie/>);
    await new Promise(resolve => setTimeout(resolve, 1000));
  });