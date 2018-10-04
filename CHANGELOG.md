# Changelog

## v0.0.1.1 (May 11, 2018)

- [ ] Initial release.

## v0.0.1.2 (May 11, 2018)

- [ ] Dragging files into app more than once merges the different entries instead of creating duplicates (both for wordlist and json files).

## v0.0.1.3 (May 12, 2018)

- [ ] Using regex for more precise wordmatching to cut back on false positives (ass=>assume, etc).

- [ ] All matching fitlered words are now logged with the entry.

- [ ] Optimized the log file writing process using StringBuilder to streamline the IO.

- [ ] Threaded the background tasks so the program doesn't freeze while crunching your fuckin data.

- [ ] Included list of word matches with each flagged entry so you know what the fuck you said wrong that time.

- [ ] Matched entries are printed in chronological order in the log.
v0.0.1.4 (May 12, 2018)

- [ ] Fixed some bugs causing search results to overlap previous ones.

- [ ] Jazzed up the UI just a smidgen.
v0.0.2.0 (May 13, 2018)

- [ ] Nothing major, just yanked out stale notes and comments that were still hanging around from when I threw all this together.

# ToDo

- [ ] Optimize search speed.
- [ ] Ghetto thread usage feels weird; convert to BackgroundWorker event handlers with next update.
