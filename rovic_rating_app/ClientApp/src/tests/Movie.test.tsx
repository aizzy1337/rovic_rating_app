import { createRoot } from "react-dom/client";
import Movie from "../components/Movie";

it('renders movie without crashing', async () => {
    const div = document.createElement('div');
    const root = createRoot(div);
    root.render(
      <Movie/>);
    await new Promise(resolve => setTimeout(resolve, 1000));
  });