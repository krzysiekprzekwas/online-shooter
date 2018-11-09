let config = {

    // These values will be overwritten by server configuration
    clientStateIntervalMilliseconds: 50,
    playerRadius: 16,
    intersectionInterval: 0.01,
    minBulletSpeed: 0.1,
    maxPlayerHealth: 100,
    extrapolation: true,

    serverTickMilliseconds: 250,
    serverTicksPerSecond: 4,

    playerDeccelerationPerTick: 0.9,
    playerSpeedPerTick: 50,
    bulletDecceleraionPerTick: 0.9
};

// Export module
if (typeof module !== "undefined" && module.hasOwnProperty("exports")) {
    module.exports = config;
}