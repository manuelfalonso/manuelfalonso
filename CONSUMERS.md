# Using this library in a project

This repo is consumed as a **git submodule** inside a host Unity project,
usually under `Assets/`. It is not a Unity project itself.

## Add it

```bash
git submodule add https://github.com/manuelfalonso/Unity-Scripts.git "Assets/Sombra Studios/Shared"
git submodule update --init --recursive
```

Dropping it into an empty Unity 6.3 project must produce **zero compile
errors**. Every optional-package dependency is guarded by `versionDefines` in
its asmdef, so nothing needs setting up in Project Settings — modules whose
package is absent compile themselves out.

## Point your AI assistant at the index

The library has 600+ scripts across 30 modules. Without a pointer, an assistant
will grep the whole tree every time someone asks "is there something for X" —
slow, expensive, and it still misses things.

Add this to the **host project's** root `CLAUDE.md` (or `AGENTS.md`). Nested
instruction files are only picked up once a tool touches that subtree, so the
pointer has to live at the project root:

```markdown
## Shared library

`Assets/Sombra Studios/Shared/` is the SombraStudios.Shared submodule: reusable
Unity systems, utilities, and patterns.

To find anything in it, grep its generated index rather than searching the tree:

    grep -i <concept> "Assets/Sombra Studios/Shared/INDEX.tsv"

Columns are `Type · Kind · Base · Namespace · Path · Gate · Summary`. Never read
INDEX.tsv whole — it is ~25k tokens. Then read the owning module's page under
`Assets/Sombra Studios/Shared/Docs/modules/`, and only then open the .cs file.
The submodule's own `CLAUDE.md` has the full conventions.
```

## Finding things by hand

| You want | Do this |
|---|---|
| A type by name or concept | `grep -i pool INDEX.tsv` |
| Everything in one module | `awk -F'\t' '$5 ~ /^Systems\//' INDEX.tsv` |
| Only the drop-on-a-GameObject components | `awk -F'\t' '$3 == "MonoBehaviour"' INDEX.tsv` |
| Only the authorable assets | `awk -F'\t' '$3 == "ScriptableObject"' INDEX.tsv` |
| Everything that needs an optional package | `awk -F'\t' 'NR>1 && $6 != "-"' INDEX.tsv` |
| A guided tour of a module | `Docs/modules/<Module>.md` |
| Published API reference | <https://manuelfalonso.github.io/manuelfalonso/> |

`INDEX.tsv` is generated from the sources and verified in CI, so it is current.
The published API site is built from committed DocFX metadata and can lag
behind — prefer the index when the two disagree.

## Gates

A type's `Gate` column names the define symbol it needs. `-` means it compiles
anywhere. A gated type simply does not exist in a project without that package,
so check the column before building on it. The symbol table is in `README.md`.

## Updating

```bash
cd "Assets/Sombra Studios/Shared"
git fetch && git checkout main && git pull
```

Then commit the submodule pointer bump in the host project. Public API here is
treated as stable — breaking changes are marked `[Obsolete]` before removal —
but you update on your own schedule, so read the log before bumping.
