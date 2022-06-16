import React from 'react';
import ProgressTemplate from './ProgressTemplate';

interface IComponentProps {
  num: number,
  total: number
}

const HabitProgress : React.FC<IComponentProps> = props => {
  return(
    <>
      <ProgressTemplate type='habit' num={props.num} total={props.total}/>
    </>
  );
}

export default HabitProgress;