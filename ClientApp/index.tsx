// ClientApp/index.tsx
import React from 'react';
import { createRoot } from 'react-dom/client';
import { components } from './components';

document.addEventListener('DOMContentLoaded', () => {
  const rootElement = document.getElementById('react-root');
  if (!rootElement) return;

  const componentName = rootElement.getAttribute('data-component');
  const propsData = rootElement.getAttribute('data-props');

  if (!componentName || !(componentName in components)) {
    console.error(`Componente "${componentName}" no encontrado.`);
    return;
  }

  let props = {};
  if (propsData) {
    try {
      props = JSON.parse(propsData);
    } catch (e) {
      console.log(propsData);
      console.error('Error al parsear data-props:', e);
    }
  }

  const Component = components[componentName];
  const root = createRoot(rootElement);
  root.render(<Component {...props} />);
});