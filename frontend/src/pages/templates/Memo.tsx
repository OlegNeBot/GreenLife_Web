import React from 'react';
import Template from './Template';

export interface ComponentProps {
  name: string,
  picRef: string,
  className?: string
};

const Memo : React.FC<ComponentProps> = props => {
  return (
    <Template type='memo' name={props.name} picRef={props.picRef} className={props.className}/>
  );
}

export default Memo;