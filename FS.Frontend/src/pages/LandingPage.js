import Page from 'src/components/Page';
import Hero from 'src/components/_external-pages/landing/Hero';
import { makeStyles } from '@material-ui/core/styles';
// ----------------------------------------------------------------------

const useStyles = makeStyles(theme => ({
  root: {
    height: '100%'
  }
}));

// ----------------------------------------------------------------------

export default function LandingPage() {
  const classes = useStyles();
  
  return (
    <Page title="Football Solutions" id="move_top" className={classes.root}>
      <Hero />
    </Page>
  );
}
