import 'primereact/resources/themes/lara-light-indigo/theme.css';
import 'primereact/resources/primereact.min.css';
import 'primeicons/primeicons.css';

import React from 'react';

import Header from '../../header';

export default props => (
    <>
      <Header />
      <div className="container">
        {props.children}
      </div>
    </>
  );