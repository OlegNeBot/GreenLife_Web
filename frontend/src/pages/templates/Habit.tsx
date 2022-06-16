import React from 'react';
import Template from './Template';

export interface ComponentProps {
  name: string,
  total: number,
  num: number,
  phrase: string,
  className?: string
};

const Habit : React.FC<ComponentProps> = props => {
  return (
    <Template type='habit' name={props.name} total={props.total} num={props.num} phrase={props.phrase} className={props.className}/>
  );
}

export default Habit;