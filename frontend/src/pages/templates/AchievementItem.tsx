import React from 'react';
import ItemTemplate from './ItemTemplate';

interface AchievementProps {
  username: string,
  date: string,
  achievement: string
}

const AchievementItem : React.FC<AchievementProps> = props => {
  return(
    <>
      <ItemTemplate type='achievement' username={props.username} date={props.date} achievement={props.achievement}/>
    </>
    
  );
}

export default AchievementItem;