import React from 'react';
import { createRoot } from 'react-dom/client';
import Start from '../components/Start';

it('renders start without crashing', async () => {
    const div = document.createElement('div');
    const root = createRoot(div);
    root.render(
      <Start/>);
    await new Promise(resolve => setTimeout(resolve, 1000));
  });