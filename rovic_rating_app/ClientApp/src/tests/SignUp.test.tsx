import { createRoot } from "react-dom/client";
import SignUp from "../components/SingUp";

it('renders signup without crashing', async () => {
    const div = document.createElement('div');
    const root = createRoot(div);
    root.render(
      <SignUp/>);
    await new Promise(resolve => setTimeout(resolve, 1000));
  });