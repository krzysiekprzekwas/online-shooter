let logger = {

    logLevels: {
        "INFO": 1,
        "DEBUG": 2,
        "WARN": 3,
        "ERROR": 4,
    },
    
    log(logLevelName, message) {
        
        if(logLevelName == "WARN")
            console.warn(message);
        else if(logLevelName == "ERROR")
            console.error(message);
        else
            console.log(message);
    },

    getFormattedDate() {

        const date = new Date();

        const datevalues = [
            date.getFullYear(),
            date.getMonth()+1,
            date.getDate(),
            ('0' + date.getHours()).slice(-2),
            ('0' + date.getMinutes()).slice(-2),
            ('0' + date.getSeconds()).slice(-2),
        ];

        return datevalues[3] + ":" + datevalues[4] + ":" + datevalues[5];
    },

    info(message) {
        this.log("INFO", message);
    },
    debug(message) {
        this.log("DEBUG", message);
    },
    warn(message) {
        this.log("WARN", message);
    },
    error(message) {
        this.log("ERROR", message);
    },

};