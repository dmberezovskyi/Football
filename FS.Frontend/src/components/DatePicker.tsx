import React from 'react'
import { DesktopDatePicker, MobileDatePicker, DatePickerProps } from '@material-ui/lab';
import MHidden from 'src/components/@material-extend/MHidden'
import { Box } from '@material-ui/core';


const DatePicker = (props: DatePickerProps) => {

    return (
        <Box>
            <MHidden width="mdDown">
                <DesktopDatePicker {...props}></DesktopDatePicker>
            </MHidden>
            <MHidden width="mdUp">
                <MobileDatePicker {...props}></MobileDatePicker>
            </MHidden>
        </Box>
    );
}

export default DatePicker;