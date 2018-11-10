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

    playerDeccelerationFactorPerTick: Math.pow(0.04, 1.0 / 4),
    playerSpeedPerTick: 150 / 4,
    bulletDecceleraionFactorPerTick: Math.pow(0.5, 1.0 / 4)
};

// Export module
if (typeof module !== "undefined" && module.hasOwnProperty("exports")) {
    module.exports = config;
}