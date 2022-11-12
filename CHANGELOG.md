# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2022-11-08

### Changed

- Restructured the package to work with Unity's Package Manager
- Remove check for other cameras at -100 depth
- Set the background camera's depth to int.MinValue
- Set the background camera's parent to this transform
- Expose function to manually resize letterboxing
