# Changelog

## v0.0.1.1 (May 11, 2018)

- [x] Initial release.

## v0.0.1.2 (May 11, 2018)

- [x] Dragging files into app more than once merges the different entries instead of creating duplicates (both for wordlist and json files).

## v0.0.1.3 (May 12, 2018)

- [x] Using regex for more precise wordmatching to cut back on false positives (ass=>assume, etc).

- [x] All matching fitlered words are now logged with the entry.

- [x] Optimized the log file writing process using StringBuilder to streamline the IO.

- [x] Threaded the background tasks so the program doesn't freeze while crunching your fuckin data.

- [x] Included list of word matches with each flagged entry so you know what the fuck you said wrong that time.

- [x] Matched entries are printed in chronological order in the log.

## v0.0.1.4 (May 12, 2018)

- [x] Fixed some bugs causing search results to overlap previous ones.

## v0.0.1.4 (May 13, 2018)

- [x] Jazzed up the UI just a smidgen.

## v0.0.2.0 (May 13, 2018)

- [x] Nothing major, just yanked out stale notes and comments that were still hanging around from when I threw all this together.

# ToDo

- [ ] Optimize search speed.
- [ ] Ghetto thread usage feels weird; convert to BackgroundWorker event handlers with next update.
