import { Icon } from '@iconify/react';
import React, { useRef, useState, memo, FunctionComponent } from 'react';
import moreVerticalFill from '@iconify-icons/eva/more-vertical-fill';
import { makeStyles } from '@material-ui/core/styles';
import {
  Menu,
  MenuItem,
  IconButton,
  ListItemIcon,
  ListItemText
} from '@material-ui/core';


const useStyles = makeStyles((theme) => ({
  menu: {
    width: 220,
    maxWidth: '100%'
  }
}));

export interface MoreButtonAction {
  name: string,
  icon: any,
  action(): void
}
interface MoreButtonProps {
  className?: string
  actions: MoreButtonAction[]
}

const MoreButton: FunctionComponent<MoreButtonProps> = ({ className, actions }) => {
  const classes = useStyles();
  const ref = useRef(null);
  const [isOpen, setIsOpen] = useState(false);

  return (
    <>
      <IconButton
        ref={ref}
        className={className}
        onClick={() => setIsOpen(true)}
      >
        <Icon icon={moreVerticalFill} width={20} height={20} />
      </IconButton>

      <Menu
        open={isOpen}
        anchorEl={ref.current}
        onClose={() => setIsOpen(false)}
        PaperProps={{ className: classes.menu }}
        anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
        transformOrigin={{ vertical: 'top', horizontal: 'right' }}
      >
        {actions.map((item) => (
          <MenuItem
            key={item.name}
            onClick={item.action}
            sx={{ color: 'text.secondary' }}
          >
            {
              item.icon &&
              <ListItemIcon>
                <Icon icon={item.icon} width={24} height={24} />
              </ListItemIcon>
            }
            <ListItemText
              primary={item.name}
              primaryTypographyProps={{ variant: 'body2' }}
            />
          </MenuItem>
        ))}
      </Menu>
    </>
  );
}

export default memo(MoreButton);
