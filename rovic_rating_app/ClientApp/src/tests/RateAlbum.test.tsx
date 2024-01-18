import { createRoot } from "react-dom/client";
import RateAlbum from "../components/RateAlbum";

it('renders ratealbum without crashing', async () => {
    const div = document.createElement('div');
    const root = createRoot(div);
    root.render(
      <RateAlbum/>);
    await new Promise(resolve => setTimeout(resolve, 1000));
  });