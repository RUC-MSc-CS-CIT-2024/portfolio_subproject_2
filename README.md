# portfolio_subproject_2
Portfolio Subproject made for the CIT 2024 course

Find the latest version at https://github.com/orgs/RUC-MSc-CS-CIT-2024/packages/container/package/portfolio_subproject_2

Set the `<version>` according to the [Semantic Versioning](https://semver.org/) standard.

```
NEW_TAG_VERSION=<version>
docker buildx build --platform linux/amd64,linux/arm64,linux/arm,linux/arm/v8 -t ghcr.io/ruc-msc-cs-cit-2024/portfolio_subproject_2:$NEW_TAG_VERSION -t ghcr.io/ruc-msc-cs-cit-2024/portfolio_subproject_2 --push .
```