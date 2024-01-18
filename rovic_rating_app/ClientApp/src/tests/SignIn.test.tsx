import { createRoot } from "react-dom/client";
import SignIn from "../components/SingIn";

it('renders signin without crashing', async () => {
    const div = document.createElement('div');
    const root = createRoot(div);
    root.render(
      <SignIn/>);
    await new Promise(resolve => setTimeout(resolve, 1000));
  });